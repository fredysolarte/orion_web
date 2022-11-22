<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ClasesParametros.aspx.cs" Inherits="XUSS.WEB.Parametros.ClasesParametros" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000" EnablePageMethods="true">
    </telerik:RadScriptManager>    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">    
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_clasesparametros" runat="server" PageSize="1" AllowPaging="True" 
            OnItemCommand="rlv_clasesparametros_OnItemCommand" OnItemDataBound="rlv_clasesparametros_OnItemDataBound" 
            DataSourceID="obj_clasesparametros" ItemPlaceholderID="pnlGeneral" DataKeyNames="CLAP_CLASE"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Clases Parametros</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_clasesparametros" PageSize="1">
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
                                <h5>
                                    Clases Parametros</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert"  />
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
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar">
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
                                <label>Cod Clase</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("CLAP_CLASE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre Clase</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("CLAP_NOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                    </table>

                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView ShowGroupFooter="true" >
                                <Columns>                            
                                    <telerik:GridBoundColumn DataField="TTCODCLA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Clave" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTCODCLA"
                                        UniqueName="PDCLAVE1">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Valor C" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORC"
                                        UniqueName="TTVALORC">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORN" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Valor N" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORN"
                                        UniqueName="TTVALORN">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORD" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Valor D" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORD"
                                        UniqueName="TTVALORD">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                        HeaderText="Estado" Resizable="true" SortExpression="TTESTADO" ItemStyle-HorizontalAlign="Right"
                                        UniqueName="TTESTADO">                                        
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>                
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Cod Clase</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("CLAP_CLASE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre Clase</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("CLAP_NOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>        
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                    ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" />
                                    <telerik:GridButtonColumn  ButtonType="ImageButton" CommandName="Edit"  HeaderStyle-Width="20px" />                    
                                    <telerik:GridBoundColumn DataField="TTCODCLA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Clave" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTCODCLA"
                                        UniqueName="PDCLAVE1">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Valor C" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORC"
                                        UniqueName="TTVALORC">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORN" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Valor N" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORN"
                                        UniqueName="TTVALORN">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTVALORD" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Valor D" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTVALORD"
                                        UniqueName="TTVALORD">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                        HeaderText="Estado" Resizable="true" SortExpression="TTESTADO" ItemStyle-HorizontalAlign="Right"
                                        UniqueName="TTESTADO">                                        
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>C. Cla</label>
                                                </td>
                                                <td>
                                                <telerik:RadTextBox ID="txt_codcla" runat="server" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' Text='<%# Bind("TTCODCLA") %>' 
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Valor C.</label>
                                                </td>
                                                <td>
                                                <telerik:RadTextBox ID="txt_vlrc" runat="server" Enabled="true" Text='<%# Bind("TTVALORC") %>'
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <label>Valor N.</label>
                                                </td>
                                                <td>
                                                <telerik:RadNumericTextBox ID="txt_vlrn" runat="server" Enabled="true" DbValue='<%# Bind("TTVALORN") %>'
                                                                    Width="300px">
                                                                </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Valor D.</label>
                                                </td>
                                                <td>
                                                <telerik:RadDatePicker ID="txt_vlrd" runat="server" Enabled="true" DbSelectedDate='<%# Bind("TTVALORC") %>'
                                                                    Width="300px">
                                                                </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <label>Descripcion</label>
                                                </td>
                                                <td>
                                                <telerik:RadTextBox ID="txt_nomcla" runat="server" Enabled="true" Text='<%# Bind("TTDESCRI") %>'
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        ValidationGroup="grNuevo" SkinID="SkinUpdateUC" />
                                                    <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                    ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_clasesparametros" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetClasesParametros" TypeName="XUSS.BLL.Comun.ComunBL"  >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
