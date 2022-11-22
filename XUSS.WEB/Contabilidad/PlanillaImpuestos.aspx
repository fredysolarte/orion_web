<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PlanillaImpuestos.aspx.cs" Inherits="XUSS.WEB.Contabilidad.PlanillaImpuestos" %>

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
        <telerik:RadListView ID="rlv_planimp" runat="server" PageSize="1" AllowPaging="True" OnItemCommand="rlv_planimp_OnItemCommand" OnItemInserted="rlv_planimp_OnItemInserted"
            OnItemDataBound="rlv_planimp_OnItemDataBound" DataSourceID="obj_planimp" ItemPlaceholderID="pnlGeneral" DataKeyNames="PH_CODIGO"
            DataSourceCount="0">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Planilla Impuestos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_planimp" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Planilla Impuestos</h5>
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
                                    <label>
                                        Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aplicar Filtro" RenderMode="Lightweight">
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
                                <label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("PH_CODIGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("PH_NOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T Planilla</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tippla" runat="server" SelectedValue='<%# Bind("PH_TIPPLA") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Ventas" Value="1" />
                                        <telerik:RadComboBoxItem Text="Compras" Value="2" />
                                        <telerik:RadComboBoxItem Text="Cta Cobro" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("PH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView ShowGroupFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                        UniqueName="PC_CODIGO">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                        UniqueName="PC_NOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PI_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_NATURALEZA"
                                        UniqueName="PI_NATURALEZA">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  
                                    <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                        HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                        UniqueName="PI_PORCENTAJE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                        HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                        UniqueName="PI_BASE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>                
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Eval("PH_CODIGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("PH_NOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_nombre" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" >
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T. PLanilla</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tippla" runat="server" SelectedValue='<%# Bind("PH_TIPPLA") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Ventas" Value="1" />
                                        <telerik:RadComboBoxItem Text="Compras" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tippla" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("PH_ESTADO") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />                                        
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_estado" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_NeedDataSource">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PI_ITEM" >
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>                                    
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                        UniqueName="PC_CODIGO">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                        UniqueName="PC_NOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="PI_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_NATURALEZA"
                                        UniqueName="PI_NATURALEZA">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                        HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                        UniqueName="PI_PORCENTAJE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                        HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                        UniqueName="PI_BASE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td><label>Cta Contable</label></td>
                                                <td colspan="3">
                                                    <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single"
                                                        DataSourceID="obj_ctacontable" Width="450px" DataTextField="PC_NOMBRE" DropDownWidth="550px" Enabled ="true"
                                                        OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="PC_CODIGO"
                                                        DropDownHeight="580px" Filter="Contains">
                                                        <DropDownItemTemplate>
                                                            <table cellspacing="1">
                                                                <tr>
                                                                    <td>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PC_CODIGO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PC_NOMBRE")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DropDownItemTemplate>
                                                    </telerik:RadAutoCompleteBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Impuesto</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px"
                                                        Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' DataSourceID="obj_impuesto" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" SelectedValue='<%# Bind("IM_IMPUESTO") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_impuesto" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>                                           
                                                <td>
                                                    <label>Porcentaje</label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_porcentaje" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("PI_PORCENTAJE") %>' >
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_porcentaje"
                                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>A. Base</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_indbase" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>'   />
                                                </td>
                                                <td>
                                                    <label>% Base</label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_base" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("PI_BASE") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label>Naturaleza</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_naturaleza" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" DataSourceID="obj_naturaleza" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PI_NATURALEZA") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_naturaleza" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
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
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>                
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("PH_CODIGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("PH_NOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_nombre" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" >
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T. PLanilla</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tippla" runat="server" SelectedValue='<%# Bind("PH_TIPPLA") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Ventas" Value="1" />
                                        <telerik:RadComboBoxItem Text="Compras" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tippla" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("PH_ESTADO") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />                                        
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_estado" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_ItemCommand" OnItemDataBound="rg_items_ItemDataBound"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_NeedDataSource">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PI_ITEM" >
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                        UniqueName="PC_CODIGO">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                        UniqueName="PC_NOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PI_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_NATURALEZA"
                                        UniqueName="PI_NATURALEZA">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                        HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                        UniqueName="PI_PORCENTAJE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                        HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                        UniqueName="PI_BASE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td><label>Cta Contable</label></td>
                                                <td colspan="3">
                                                    <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single"
                                                        DataSourceID="obj_ctacontable" Width="450px" DataTextField="PC_NOMBRE" DropDownWidth="550px" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                                        OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="PC_CODIGO"
                                                        DropDownHeight="580px" Filter="Contains">
                                                        <DropDownItemTemplate>
                                                            <table cellspacing="1">
                                                                <tr>
                                                                    <td>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PC_CODIGO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PC_NOMBRE")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DropDownItemTemplate>
                                                    </telerik:RadAutoCompleteBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Impuesto</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px"
                                                         DataSourceID="obj_impuesto" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" SelectedValue='<%# Bind("IM_IMPUESTO") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_impuesto" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>                                            
                                                <td>
                                                    <label>Porcentaje</label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_porcentaje" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("PI_PORCENTAJE") %>' >
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_porcentaje"
                                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>A. Base</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_indbase" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>'   />
                                                </td>
                                                <td>
                                                    <label>Base</label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_base" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("PI_BASE") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label>Naturaleza</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_naturaleza" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" DataSourceID="obj_naturaleza" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PI_NATURALEZA") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_naturaleza" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
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
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
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
    <asp:ObjectDataSource ID="obj_planimp" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_planimp_OnInserting" OnUpdating="obj_planimp_OnUpdating" OnInserted="obj_planims_OnInserted"
        SelectMethod="GetPlanillaImpuestosHD" TypeName="XUSS.BLL.Contabilidad.PlanillaImpuestosBL" InsertMethod="InsertPlanillaImpuestosHD" UpdateMethod="UpdatePlanillaImpuestosHD">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" /> 
            <asp:Parameter Name="PH_NOMBRE" Type="String" /> 
            <asp:Parameter Name="PH_ESTADO" Type="String" /> 
            <asp:Parameter Name="PH_TIPPLA" Type="String" /> 
            <asp:Parameter Name="inDT" Type="Object" /> 
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />             
            <asp:Parameter Name="PH_NOMBRE" Type="String" /> 
            <asp:Parameter Name="PH_ESTADO" Type="String" /> 
            <asp:Parameter Name="PH_TIPPLA" Type="String" /> 
            <asp:Parameter Name="inDT" Type="Object" /> 
            <asp:Parameter Name="original_PH_CODIGO" Type="Int32" /> 
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_impuesto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="IMPF" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_ctacontable" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_naturaleza" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="NATUR" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
