<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="LstTareas.aspx.cs" Inherits="XUSS.WEB.Tareas.LstTareas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function rgDetalle_OnRowDblClick(sender, eventArgs) {
            //var modal = $find('MEditTicket');  //haciendo referencia al behavior, y NO al id
            //modal.show();
            document.getElementById("ctl00_ContentPlaceHolder1_toEditRow").value = eventArgs.get_itemIndexHierarchical();
            __doPostBack("ModalEdit", "");

        }
        function rgAsigandos_OnRowDblClick(sender, eventArgs) {
            //var modal = $find('MEditTicket');  //haciendo referencia al behavior, y NO al id
            //modal.show();
            document.getElementById("ctl00_ContentPlaceHolder1_toEditRow").value = eventArgs.get_itemIndexHierarchical();
            __doPostBack("ModalEditAsig", "");

        }
    </script>
    <input type="text" id="toEditRow" name="toEditRow" value="-1" style="display: none"
        runat="server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="rlpCirculos" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="rlpCirculos">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Web20" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Listado de Tareas</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="SkinAddUC" OnClick="im_nuevo"
                ToolTip="Nuevo Ticket" />
        </div>
        <div>
            <fieldset class="cssFieldSetContainer">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Pendientes">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Asignados">
                        </telerik:RadTab>
                        <%--<telerik:RadTab Text="Ejecutadas" Selected="True">
                        </telerik:RadTab>--%>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_tareas">
                                <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                    Skin="Web20" Culture="(Default)" CellSpacing="0" DataSourceID="obj_LstTareasAC"
                                    OnUpdateCommand="rgDetalle_OnUpdateCommand" OnDeleteCommand="rgDetalle_OnDeleteCommand"
                                    OnPreRender="rgDetalle_PreRender" AllowFilteringByColumn="True" AllowSorting="true" ClientSettings-Scrolling-AllowScroll="true">
                                    <MasterTableView DataSourceID="obj_LstTareasAC" DataKeyNames="TK_NUMERO" AllowFilteringByColumn="True" AllowSorting="true">
                                        <Columns>
                                            <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" />--%>
                                            <telerik:GridButtonColumn ConfirmText="Desea Borrar la Tarea?" ConfirmDialogType="RadWindow"
                                                HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                                ConfirmDialogHeight="100px" ConfirmDialogWidth="220px" Visible="false" />
                                            <telerik:GridBoundColumn DataField="TK_NUMERO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                AllowFiltering="false" HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="TK_NUMERO">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TK_ASUNTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Asunto" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_ASUNTO">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="TK_PRIORIDAD" HeaderText="Prioridad" UniqueName="TK_PRIORIDAD"
                                                HeaderStyle-Width="50px" AllowFiltering="false" SortExpression="TK_PRIORIDAD">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_prioridadLabel" runat="server" Text='<%# this.GetPrioridad(Convert.ToString(Eval("TK_PRIORIDAD"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_PROPIETARIO" HeaderText="Propietario" UniqueName="TK_PROPIETARIO"
                                                HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="TK_PROPIETARIO">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_propietarioLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_PROPIETARIO"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_RESPONSABLE" HeaderText="Responsable" UniqueName="TK_RESPONSABLE"
                                                HeaderStyle-Width="120px" FilterControlWidth="80px" SortExpression="TK_RESPONSABLE">
                                                <FilterTemplate>
                                                    <telerik:RadComboBox ID="RadComboBox1" DataSourceID="obj_filtro" DataTextField="usua_nombres"
                                                        DataValueField="TK_RESPONSABLE" Height="80px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TK_RESPONSABLE").CurrentFilterValue %>'
                                                        runat="server" OnClientSelectedIndexChanged="SelectedIndexChanged">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Todos" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                                        <script type="text/javascript">
                                                            function SelectedIndexChanged(sender, args) {
                                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                tableView.filter("TK_RESPONSABLE", args.get_item().get_value(), "EqualTo");
                                                            }  
                                                        </script>
                                                    </telerik:RadScriptBlock>
                                                </FilterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_responsableLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_RESPONSABLE"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_OBSERVACIONES" HeaderText="Obs." HeaderStyle-Width="40px"
                                                AllowFiltering="false" UniqueName="TK_OBSERVACIONES">
                                                <ItemTemplate>
                                                    <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                                        TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                                        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                                            <div class="box">
                                                                <div class="title">
                                                                    <h5>
                                                                        Observación
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                            <div style="padding: 5px 5px 5px 5px">
                                                                <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                                    Text='<%# Eval("TK_OBSERVACIONES") %>' Width="400px" Rows="5"></asp:TextBox>
                                                                <div style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </asp:Panel>
                                                    <asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_ESTADO" HeaderText="Estado" UniqueName="TK_ESTADO"
                                                HeaderStyle-Width="50px" AllowFiltering="false" SortExpression="TK_ESTADO">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_estadoLabel" runat="server" Text='<%# this.GetEstado(Convert.ToString(Eval("TK_ESTADO"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="TK_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Fecha Ing." ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_FECING" DataFormatString="{0:MM/dd/yyyy}">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TK_FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Fecha Ven." ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_FECVEN" DataFormatString="{0:MM/dd/yyyy}">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OBS_TK" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="OBS_TK" AllowSorting="true">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Estado</label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="estadoLabel" runat="server" Checked='<%# Eval("TK_ESTADO").ToString()=="AC"?true:false %>'>
                                                            </asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                            </asp:Button>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button2" runat="server" Text="Cancelar" CausesValidation="false"
                                                                CommandName="Cancel"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
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
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
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
                                        <ClientEvents OnRowDblClick="rgDetalle_OnRowDblClick" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                                    </ClientSettings>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </div>
                        <asp:ObjectDataSource ID="obj_LstTareasAC" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetLstTareas" TypeName="XUSS.BLL.Tareas.LstTareasBL" UpdateMethod="UpdateTicket">
                            <SelectParameters>
                                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                <asp:Parameter Name="Usuario" Type="String" />
                                <asp:Parameter DefaultValue="AC" Name="Estado" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                <asp:Parameter Name="TK_NUMERO" Type="Int32" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="Panel1">
                                <telerik:RadGrid ID="rg_asignados" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                    Skin="Web20" Culture="(Default)" CellSpacing="0" DataSourceID="obj_LstTareasAsiganadas" 
                                    OnUpdateCommand="rgDetalle_OnUpdateCommand" OnDeleteCommand="rgDetalle_OnDeleteCommand"
                                    OnPreRender="rg_asignados_PreRender" AllowFilteringByColumn="True" AllowSorting="true">
                                    <MasterTableView DataSourceID="obj_LstTareasAsiganadas" DataKeyNames="TK_NUMERO" AllowFilteringByColumn="True" AllowSorting="true">
                                        <Columns>
                                            <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" />--%>
                                            <telerik:GridButtonColumn ConfirmText="Desea Borrar la Tarea?" ConfirmDialogType="RadWindow"
                                                HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                                ConfirmDialogHeight="100px" ConfirmDialogWidth="220px" Visible="false"/>
                                            <telerik:GridBoundColumn DataField="TK_NUMERO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                AllowFiltering="false" HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="TK_NUMERO" AllowSorting="true">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TK_ASUNTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Asunto" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_ASUNTO" AllowSorting="true">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="TK_PRIORIDAD" HeaderText="Prioridad" UniqueName="TK_PRIORIDAD"
                                                HeaderStyle-Width="50px" AllowFiltering="false" SortExpression="TK_PRIORIDAD">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_prioridadLabel" runat="server" Text='<%# this.GetPrioridad(Convert.ToString(Eval("TK_PRIORIDAD"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_PROPIETARIO" HeaderText="Propietario" UniqueName="TK_PROPIETARIO"
                                                HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="TK_PROPIETARIO">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_propietarioLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_PROPIETARIO"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_RESPONSABLE" HeaderText="Responsable" UniqueName="TK_RESPONSABLE"
                                                HeaderStyle-Width="120px" FilterControlWidth="80px" SortExpression="TK_RESPONSABLE">
                                                <FilterTemplate>
                                                    <telerik:RadComboBox ID="RadComboBox1" DataSourceID="obj_filtro" DataTextField="usua_nombres"
                                                        DataValueField="TK_RESPONSABLE" Height="80px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TK_RESPONSABLE").CurrentFilterValue %>'
                                                        runat="server" OnClientSelectedIndexChanged="SelectedIndexChanged">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Todos" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                                        <script type="text/javascript">
                                                            function SelectedIndexChanged(sender, args) {
                                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                tableView.filter("TK_RESPONSABLE", args.get_item().get_value(), "EqualTo");
                                                            }  
                                                        </script>
                                                    </telerik:RadScriptBlock>
                                                </FilterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_responsableLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_RESPONSABLE"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="TK_OBSERVACIONES" HeaderText="Obs." HeaderStyle-Width="40px"
                                                AllowFiltering="false" UniqueName="TK_OBSERVACIONES">
                                                <ItemTemplate>
                                                    <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                                        TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                                        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                                            <div class="box">
                                                                <div class="title">
                                                                    <h5>
                                                                        Observación
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                            <div style="padding: 5px 5px 5px 5px">
                                                                <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                                    Text='<%# Eval("TK_OBSERVACIONES") %>' Width="400px" Rows="5"></asp:TextBox>
                                                                <div style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </asp:Panel>
                                                    <asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridTemplateColumn>                                            
                                            <telerik:GridTemplateColumn DataField="TK_ESTADO" HeaderText="Estado" UniqueName="TK_ESTADO"
                                                HeaderStyle-Width="50px" AllowFiltering="false" SortExpression="TK_ESTADO">
                                                <ItemTemplate>
                                                    <asp:Label ID="tk_estadoLabel" runat="server" Text='<%# this.GetEstado(Convert.ToString(Eval("TK_ESTADO"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="TK_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Fecha Ing." ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_FECING" DataFormatString="{0:MM/dd/yyyy}" >
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TK_FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Fecha Ven." ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TK_FECVEN" DataFormatString="{0:MM/dd/yyyy}">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OBS_TK" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="OBS_TK" AllowSorting="true">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Estado</label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="estadoLabel" runat="server" Checked='<%# Eval("TK_ESTADO").ToString()=="AC"?true:false %>'>
                                                            </asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                            </asp:Button>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button2" runat="server" Text="Cancelar" CausesValidation="false"
                                                                CommandName="Cancel"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
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
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
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
                                        <ClientEvents OnRowDblClick="rgAsigandos_OnRowDblClick" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                                    </ClientSettings>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </div>
                        <asp:ObjectDataSource ID="obj_LstTareasAsiganadas" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetLstTareasAsig" TypeName="XUSS.BLL.Tareas.LstTareasBL" UpdateMethod="UpdateTicket">
                            <SelectParameters>
                                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                <asp:Parameter Name="Usuario" Type="String" />
                                <asp:Parameter DefaultValue="AC" Name="Estado" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                <asp:Parameter Name="TK_NUMERO" Type="Int32" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </telerik:RadPageView>
                   <%-- <telerik:RadPageView ID="RadPageView2" runat="server">
                        <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" Width="680px" AutoGenerateColumns="False"
                            Skin="Web20" Culture="(Default)" CellSpacing="0" DataSourceID="obj_LstTareasCE"
                            OnPreRender="RadGrid1_PreRender">
                            <MasterTableView DataSourceID="obj_LstTareasCE">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TK_NUMERO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Numero" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TK_NUMERO">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TK_ASUNTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Asunto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TK_ASUNTO">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="TK_PRIORIDAD" HeaderText="Prioridad" UniqueName="column6">
                                        <ItemTemplate>
                                            <asp:Label ID="tk_prioridadLabel" runat="server" Text='<%# this.GetPrioridad(Convert.ToString(Eval("TK_PRIORIDAD"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TK_PROPIETARIO" HeaderText="Propietario" UniqueName="column7">
                                        <ItemTemplate>
                                            <asp:Label ID="tk_propietarioLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_PROPIETARIO"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TK_RESPONSABLE" HeaderText="Responsable" UniqueName="column8">
                                        <ItemTemplate>
                                            <asp:Label ID="tk_responsableLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_RESPONSABLE"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TK_OBSERVACIONES" HeaderText="Observaciónes"
                                        UniqueName="column9">
                                        <ItemTemplate>
                                            <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                                TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                                <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                                    <div class="box">
                                                        <div class="title">
                                                            <h5>
                                                                Observación
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                            Text='<%# Eval("TK_OBSERVACIONES") %>' Width="400px" Rows="5"></asp:TextBox>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </asp:Panel>
                                            <asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TK_ESTADO" HeaderText="Estado" UniqueName="column8">
                                        <ItemTemplate>
                                            <asp:Label ID="tk_estadoLabel" runat="server" Text='<%# this.GetEstado(Convert.ToString(Eval("TK_ESTADO"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="TK_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Fecha Ing." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TK_FECING"
                                        DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
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
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                            </HeaderContextMenu>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="obj_LstTareasCE" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetLstTareas" TypeName="XUSS.BLL.Tareas.LstTareasBL" UpdateMethod="UpdateTicket">
                            <SelectParameters>
                                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                <asp:Parameter Name="Usuario" Type="String" />
                                <asp:Parameter DefaultValue="CE" Name="Estado" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </telerik:RadPageView>--%>
                </telerik:RadMultiPage>
            </fieldset>
        </div>
        <%--</telerik:RadAjaxPanel>--%>
        <asp:ModalPopupExtender ID="ModalPopupNuevoTicket" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="Mticket" PopupControlID="pnPopUp" TargetControlID="Button2">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button2" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnPopUp" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 590px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Nuevo Ticket</h5>
                    </div>
                </div>
                <div style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    De</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_propietario" runat="server" DataSourceID="obj_propietario"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_propietario" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_propietario" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Asunto</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="edt_asunto" runat="server" Width="412px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="edt_asunto" ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Area</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_area" runat="server" DataSourceID="obj_area" OnSelectedIndexChanged="rc_area_OnSelectedIndexChanged"
                                    DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rc_area" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_area" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAreas" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>

                            <td>
                                <label>
                                    Responsable</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_responsable" runat="server" DataSourceID="obj_responsable"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_responsable" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                        <asp:Parameter Name="area" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <label>
                                    Prioridad</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_prioridad" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Bajo" Value="01" />
                                        <telerik:RadComboBoxItem Text="Medio" Value="02" />
                                        <telerik:RadComboBoxItem Text="Alto" Value="03" />
                                        <telerik:RadComboBoxItem Text="Urgente" Value="04" />
                                        <telerik:RadComboBoxItem Text="Emergencia" Value="05" />
                                        <telerik:RadComboBoxItem Text="Critico" Value="06" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_prioridad" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>

                            <td>
                                <label>
                                    Vencimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecVencimiento" runat="server">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="edt_fecVencimiento" ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Descripción</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <telerik:RadTextBox ID="edt_Observaciones" runat="server" TextMode="MultiLine" Width="520px" Height ="250">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Adjunto</label></td>
                            <td colspan="4">
                                <telerik:RadAsyncUpload ID="rauArchivo" runat="server" MaxFileInputsCount="1">                                    
                                </telerik:RadAsyncUpload>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <div style="text-align: right;">
                <asp:Button ID="btnAgregar" runat="server" Text="Aceptar" OnClick="btnAgregar_click"
                    ValidationGroup="PostBackBoton" CausesValidation="true" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_click" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupEditTicket" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnPopUpEdit" TargetControlID="Button3">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnPopupEdit" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 800px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Informacion Tarea</h5>
                    </div>
                </div>
                <div style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    De</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="erb_propietario" runat="server" DataSourceID="obj_propietario"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="Erb_estado" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Pendiente" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Ejecutado" Value="CE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Asunto</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="Eedt_asunto" runat="server" Width="412px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Area</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="Erc_area" runat="server" DataSourceID="obj_area" OnSelectedIndexChanged="Erc_area_OnSelectedIndexChanged"
                                    DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rc_area" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                                
                            </td>
                            <td>
                                <label>
                                    Responsable</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="erb_responsable" runat="server" DataSourceID="obj_responsable"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Prioridad</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="erb_prioridad" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Bajo" Value="01" />
                                        <telerik:RadComboBoxItem Text="Medio" Value="02" />
                                        <telerik:RadComboBoxItem Text="Alto" Value="03" />
                                        <telerik:RadComboBoxItem Text="Urgente" Value="04" />
                                        <telerik:RadComboBoxItem Text="Emergencia" Value="05" />
                                        <telerik:RadComboBoxItem Text="Critico" Value="06" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>                            
                        </tr>                        
                    </table>
                    <telerik:RadGrid ID="rg_detalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                        Skin="Web20" Culture="(Default)" AllowAutomaticDeletes="True"
                        DataSourceID="obj_detalle" CellSpacing="0" ShowStatusBar="True" 
                        MasterTableView-EditFormSettings-EditColumn-InsertText="Aceptar" 
                        onitemcommand="rg_detalle_ItemCommand" 
                        oninsertcommand="rg_detalle_InsertCommand">
                        <MasterTableView AutoGenerateColumns="False" InsertItemDisplay="Top" CommandItemDisplay="Top">
                            <CommandItemSettings AddNewRecordText="Nuevo Registro" ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            </ExpandCollapseColumn>
                            <Columns>
                                <%--<telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                    HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                    UniqueName="usua_nombres">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn DataField="TD_OBSERVACION" HeaderButtonType="TextButton"
                                    HeaderStyle-Width="250px" HeaderText="Observacion" ItemStyle-HorizontalAlign="Right"
                                    Resizable="true" SortExpression="TD_OBSERVACION" UniqueName="TD_OBSERVACION">
                                    <HeaderStyle Width="250px" />
                                    <ItemStyle HorizontalAlign="Right" />                                    
                                </telerik:GridBoundColumn>--%>

                                <telerik:GridTemplateColumn Visible="true" HeaderText="Usuario" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbl_usuario" runat="server" Width="40px" Text='<%# Eval("usua_nombres")%>'>
                                                </asp:Label>
                                            </div>
                                        </ItemTemplate>    
                                         <%--<InsertItemTemplate>
                                            <div>
                                                <telerik:RadTextBox ID="txtValor" runat="server" Text='<%#Bind("usua_nombres") %>' TextMode="MultiLine" Width="600px" Visible="true"/>
                                            </div>
                                        </InsertItemTemplate>  --%>                                  
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn Visible="true" HeaderText="Observaciones" >
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbl_Observacion" runat="server" Width="250px" Text='<%# Eval("TD_OBSERVACION")%>'>
                                                </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <InsertItemTemplate>
                                            <div>
                                                <telerik:RadTextBox ID="edt_observacion" runat="server" Text='<%#Bind("TD_OBSERVACION") %>' TextMode="MultiLine" Width="600px" Height="200px"/>
                                            </div>
                                        </InsertItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn Visible="true" HeaderText="Fec. Ingreso" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbl_fecingreso" runat="server" Width="80px" Text='<%# Eval("TD_FECING")%>'>
                                                </asp:Label>
                                            </div>
                                        </ItemTemplate>                                                
                                        <%--<InsertItemTemplate>
                                            <div>
                                                <telerik:RadDatePicker ID="edt_fecing" runat="server"  DbSelectedDate='<%#Bind("TD_FECING") %>'  Width="600px" Visible="true"/>
                                            </div>
                                        </InsertItemTemplate>  --%>                              
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="TD_RUTA" HeaderText="Adjunto" UniqueName="TD_RUTA"
                                                HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="TD_RUTA">
                                                <ItemTemplate>
                                                    <asp:HyperLink Target="_blank" ID="hpl_ruta" runat="server" NavigateUrl='<%#  this.GetNombreArchivoRuta(Convert.ToString(Eval("TD_RUTA")))%>' Text='<%#  this.GetNombreArchivo(Convert.ToString(Eval("TD_RUTA")))%>'></asp:HyperLink>
                                             </ItemTemplate>
                                             <InsertItemTemplate>                        
                                             <div>
                                                  <telerik:RadAsyncUpload ID="rauArchivoD" runat="server" MaxFileInputsCount="1">                                    
                                                </telerik:RadAsyncUpload>                                            
                                            </div>                                                                    
                                        </InsertItemTemplate>      
                                    <%--<a href="../Uploads/1113086977-336JARAVCLOBOG23A_All.pdf">../Uploads/1113086977-336JARAVCLOBOG23A_All.pdf</a>--%>
                                            </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn DataField="TD_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                    HeaderText="Fec. Ingreso" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                    SortExpression="TD_FECING" UniqueName="TD_FECING">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>--%>
                            </Columns>
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
                            <EditFormSettings>
                                <EditColumn ButtonType="PushButton" FilterControlAltText="Filter EditCommandColumn column" InsertText="Aceptar" CancelText="Cancelar">
                                </EditColumn>                                
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                        <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                        </HeaderContextMenu>
                    </telerik:RadGrid>
                </div>
            </fieldset>
            <div style="text-align: right;">
                <asp:Button ID="btnUpdate" runat="server" Text="Aceptar" OnClick="btnUpdate_click"
                    ValidationGroup="UpdateBoton" CausesValidation="true" />
                <asp:Button ID="Button5" runat="server" Text="Cerrar" OnClick="btnCancel_click" />
            </div>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_filtro" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarioXticket" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_detalle" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDetalleTicket" TypeName="XUSS.BLL.Tareas.LstTareasBL" InsertMethod="InsertDetalleTicket" 
        oninserting="obj_detalle_Inserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="TK_NUMERO" Type="Int32" DefaultValue="0" />
        </SelectParameters>
        <InsertParameters>            
            <asp:Parameter Name="TK_NUMERO" Type="Int32" DefaultValue="0"/>
            <asp:Parameter Name="TD_RESPONSABLE" Type="String" DefaultValue=""/>
            <asp:Parameter Name="TD_OBSERVACION" Type="String" DefaultValue=""/>            
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
