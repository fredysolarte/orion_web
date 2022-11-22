<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CargueGestion.aspx.cs" Inherits="XUSS.WEB.Gestion.CargueGestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('<%=RadAjaxManager.GetCurrent(Page).ClientID%>').ajaxRequestWithTarget('<%=rauCargar.UniqueID %>', '')
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rauCargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauCargar" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" />
                    <telerik:AjaxUpdatedControl ControlID="RadProgressArea1" />
                    <telerik:AjaxUpdatedControl ControlID="RadProgressManager1" />
                    <telerik:AjaxUpdatedControl ControlID="litTextoMensaje" UpdatePanelHeight="" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rc_eps">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauCargar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rc_tarchivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauCargar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <fieldset class="cssFieldSetContainer">
        <div class="box">
            <div class="title">
                <h5>
                    Cargue Archivo Gestion</h5>
            </div>
        </div>
        <asp:Panel ID="pnl_cabecera" runat="server">
            <table>                              
                <tr>
                    <td>
                        <telerik:RadAsyncUpload ID="rauCargar" runat="server" ControlObjectsVisibility="None"
                            Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                            OnFileUploaded="rauCargar_FileUploaded" OnClientFileUploaded="fileUploaded" Width="350px"
                            Style="margin-bottom: 0px">
                            <Localization Select="Cargar Archivo" />
                        </telerik:RadAsyncUpload>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_detalle" runat="server">
            <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
            <telerik:RadProgressArea ID="RadProgressArea1" runat="server" />
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="950px" AutoGenerateColumns="False"
                Culture="(Default)" CellSpacing="0">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                    </Scrolling>
                </ClientSettings>
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Campo1" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                            HeaderText="Fila" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="Campo1"
                            UniqueName="Campo1">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Campo2" HeaderButtonType="TextButton" HeaderText="Mesaje"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="Campo2" UniqueName="Campo2">
                            <HeaderStyle Width="200px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
    </fieldset>
    <asp:ModalPopupExtender ID="mpMensajes" runat="server" BackgroundCssClass="modalBackground"
        BehaviorID="MEditTicket" PopupControlID="pnMensaje" TargetControlID="Button3"
        CancelControlID="bt_cerrar">
    </asp:ModalPopupExtender>
    <div style="display: none;">
        <asp:Button ID="Button3" runat="server" Text="Button" />
    </div>
    <asp:Panel ID="pnMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
        <fieldset class="cssFieldSetContainer" style="width: 700px !important">
            <div class="box">
                <div class="title">
                    <h5>
                        Mensaje</h5>
                </div>
            </div>
            <div style="padding: 5px 5px 5px 5px">
                <ul>
                    <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                </ul>
                <div style="text-align: center;">
                    <asp:Button ID="bt_cerrar" runat="server" Text="Cerrar" />
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    <asp:ObjectDataSource ID="obj_tarchivo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TINGRESO" />
        </SelectParameters>
    </asp:ObjectDataSource>    
</asp:Content>