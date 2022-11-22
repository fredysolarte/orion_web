<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Scheduler.aspx.cs" Inherits="XUSS.WEB.Tareas.Scheduler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnClientRecurrenceActionDialogShowing(sender, args) {
            if (args.get_recurrenceAction() == 2) {
                args.set_editSeries(true);
                var $ = $telerik.$;
                setTimeout(function () {
                    $(".rsModalInner .rsModalContent :radio:first").hide();
                    $(".rsModalInner .rsModalContent :radio").attr("checked", "checked");
                }, 100);
            }
        }

    </script>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <%--OnClientRecurrenceActionDialogShowing="OnClientRecurrenceActionDialogShowing"--%>
        <asp:Panel ID="pnl_principal" runat="server" Width="100%" Height="100%">
            <table>
                <tr>
                    <td>
                        <label>Persona</label></td>
                    <td>
                        <telerik:RadComboBox ID="rc_responsable" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" OnSelectedIndexChanged="rc_responsable_SelectedIndexChanged" AutoPostBack="true"
                            DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadScheduler RenderMode="Lightweight" runat="server" ID="RadScheduler1" SelectedView="MonthView" OnAppointmentCommand="RadScheduler1_AppointmentCommand" OnFormCreated="RadScheduler1_FormCreated" OnAppointmentCreated="RadScheduler1_AppointmentCreated"
                            DayStartTime="08:00:00" DayEndTime="18:00:00" DataSourceID="obj_consulta" Width="100%" Height="100%" on
                            DataKeyField="id" DataSubjectField="descripcion" DataStartField="inicio" DataEndField="final" StartInsertingInAdvancedForm="true" OnAppointmentUpdate="RadScheduler1_AppointmentUpdate"
                            DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentID" Localization-ConfirmDeleteText="¿Esta Seguro Borrar el Evento?" Localization-ConfirmDeleteTitle="Confirmar!"
                            OverflowBehavior="Auto">
                            <Localization AdvancedEditAppointment="Editar" AdvancedNewAppointment="Nuevo" />
                            <%--<AdvancedForm Modal="true"></AdvancedForm>--%>
                            <AppointmentContextMenus>
                                <%--The appointment context menu interaction is handled on the client in this example--%>
                                <%--See the JavaScript code above--%>
                                <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu">
                                    <Items>
                                        <telerik:RadMenuItem Text="Abrir Evento" Value="CommandEdit">
                                        </telerik:RadMenuItem>
                                        <telerik:RadMenuItem IsSeparator="True">
                                        </telerik:RadMenuItem>
                                        <telerik:RadMenuItem Text="Categorize">
                                            <Items>
                                                <telerik:RadMenuItem Text="Work" Value="1">
                                                </telerik:RadMenuItem>
                                                <telerik:RadMenuItem Text="Marketing" Value="2">
                                                </telerik:RadMenuItem>
                                            </Items>
                                        </telerik:RadMenuItem>
                                        <telerik:RadMenuItem IsSeparator="True">
                                        </telerik:RadMenuItem>

                                        <telerik:RadMenuItem Text="Eliminar Evento" Value="CommandDelete" ImageUrl="../App_Themes/Tema2/Images/delete_.gif">
                                        </telerik:RadMenuItem>

                                    </Items>
                                </telerik:RadSchedulerContextMenu>
                            </AppointmentContextMenus>
                            <TimeSlotContextMenus>
                                <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerTimeSlotContextMenu">
                                    <Items>
                                        <telerik:RadMenuItem Text="Nuevo Evento" ImageUrl="../App_Themes/Tema2/Images/AddRecord_.gif" Value="CommandAddAppointment">
                                        </telerik:RadMenuItem>
                                        <telerik:RadMenuItem IsSeparator="true">
                                        </telerik:RadMenuItem>
                                        <%-- Custom command --%>
                                        <telerik:RadMenuItem Text="Group by Type" Value="EnableGrouping">
                                        </telerik:RadMenuItem>
                                    </Items>
                                </telerik:RadSchedulerContextMenu>

                            </TimeSlotContextMenus>
                            <ResourceStyles>
                                <telerik:ResourceStyleMapping Type="Type" Text="Marketing" ApplyCssClass="rsCategoryOrange"></telerik:ResourceStyleMapping>
                                <telerik:ResourceStyleMapping Type="Type" Text="Work" ApplyCssClass="rsCategoryGreen"></telerik:ResourceStyleMapping>
                            </ResourceStyles>
                            <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                            <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>
                            <AdvancedInsertTemplate>
                                <div id="qsfexAdvEditWrapper">
                                    <div id="qsfexAdvEditInnerWrapper">
                                        <div class="qsfexAdvAppType">
                                        </div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="TitleTextBox" runat="server" CssClass="inline-label">Description</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TitleTextBox" Rows="5" Columns="20" runat="server" Text='<%# Bind("Subject") %>'
                                                        Width="97%" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TitleTextBox"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="StartInput" runat="server" CssClass="inline-label">Inicio</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="StartInput" SelectedDate='<%# Bind("Start") %>' runat="server" Width="300px">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td>

                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Fin</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="EndInput" SelectedDate='<%# Bind("End") %>' runat="server" Width="300px">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Responsable</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rb_responsable" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" Enabled="false"
                                                        DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Serivio</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tservicio" runat="server"  Width="300px" AllowCustomText="true" Enabled="true"
                                                         AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                            <telerik:RadComboBoxItem Text="Desmonte Definitivo" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Servicio Tecnivo" Value="2" />
                                                            <telerik:RadComboBoxItem Text="Desmonte Remodelacion" Value="3" />
                                                            <telerik:RadComboBoxItem Text="Instalacion Remodelacion" Value="4" />                                                            
                                                            <telerik:RadComboBoxItem Text="Inspeccion" Value="5" />                                                            
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Proyecto</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tercero" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true" OnSelectedIndexChanged="rc_tercero_SelectedIndexChanged" AutoPostBack="true"
                                                        DataTextField="NOM_COMPLETO" DataValueField="TRCODTER" AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">P. Horizontal</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_propiedad" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true" AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>                                            
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btnInsert" runat="server" CommandName="Insert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" Width="110px" />
                                                    <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" Width="110px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </AdvancedInsertTemplate>
                            <AdvancedEditTemplate>
                                <div id="qsfexAdvEditWrapper">
                                    <div id="qsfexAdvEditInnerWrapper">
                                        <div class="qsfexAdvAppType">
                                        </div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="TitleTextBox" runat="server" CssClass="inline-label">Description</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TitleTextBox" Rows="5" Columns="20" runat="server" Text='<%# Bind("Subject") %>'
                                                        Width="97%" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TitleTextBox"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="StartInput" runat="server" CssClass="inline-label">Inicio</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="StartInput" SelectedDate='<%# Bind("Start") %>' runat="server" Width="300px">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td>

                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Fin</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="EndInput" SelectedDate='<%# Bind("End") %>' runat="server" Width="300px">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Responsable</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rb_responsable" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" Enabled="true"
                                                        DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Serivio</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tservicio" runat="server"  Width="300px" AllowCustomText="true" Enabled="false"
                                                         AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                            <telerik:RadComboBoxItem Text="Desmonte Definitivo" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Servicio Tecnivo" Value="2" />
                                                            <telerik:RadComboBoxItem Text="Desmonte Remodelacion" Value="3" />
                                                            <telerik:RadComboBoxItem Text="Instalacion Remodelacion" Value="4" />                                                            
                                                            <telerik:RadComboBoxItem Text="Inspeccion" Value="5" />                                                            
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">Proyecto</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tercero" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true" OnSelectedIndexChanged="rc_tercero_SelectedIndexChanged" AutoPostBack="true"
                                                        DataTextField="NOM_COMPLETO" DataValueField="TRCODTER" AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label AssociatedControlID="EndInput" runat="server" CssClass="inline-label">P. Horizontal</asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_propiedad" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true"  AppendDataBoundItems="true" Filter="StartsWith">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="gvInsert"
                                                        ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="">
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label>Nro Ticket</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_nroticket" runat="server" Enabled="false" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" Width="110px" />
                                                    <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" Width="110px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </AdvancedEditTemplate>
                        </telerik:RadScheduler>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
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
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertAppoiment" UpdateMethod="UpdateAppoiment" DeleteMethod="DeleteAppoiment"
        SelectMethod="GetAppoiment" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="RangeStart" Type="DateTime" DefaultValue="1900/1/1" />
            <asp:Parameter Name="RangeEnd" Type="DateTime" DefaultValue="2900/1/1" />
            <asp:Parameter Name="inUsuario" Type="String"  />
            <asp:Parameter Name="inFiltro" Type="String"  />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="descripcion" Type="String" />
            <asp:Parameter Name="inicio" Type="DateTime" />
            <asp:Parameter Name="final" Type="DateTime" />
            <asp:Parameter Name="RoomID" Type="Int32" />
            <asp:SessionParameter Name="usuario" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="RecurrenceRule" Type="String" />
            <asp:Parameter Name="RecurrenceParentID" Type="String" />
            <asp:Parameter Name="tipo" Type="Int32" />
            <asp:Parameter Name="usuario_responsable" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="PH_CODIGO" Type="Int32" />
            <asp:Parameter Name="SERVICIO" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="descripcion" Type="String" />
            <asp:Parameter Name="inicio" Type="DateTime" />
            <asp:Parameter Name="final" Type="DateTime" />
            <asp:Parameter Name="RoomID" Type="Int32" />
            <asp:Parameter Name="usuario" Type="String"  />
            <asp:Parameter Name="RecurrenceRule" Type="String" />
            <asp:Parameter Name="RecurrenceParentID" Type="String" />
            <asp:Parameter Name="tipo" Type="Int32" />
            <asp:Parameter Name="usuario_responsable" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="TK_NUMERO" Type="Int32" />
            <asp:Parameter Name="PH_CODIGO" Type="Int32" />
            <asp:Parameter Name="SERVICIO" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="id" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_usuarios" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_clientes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TRINDCLI='S'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
