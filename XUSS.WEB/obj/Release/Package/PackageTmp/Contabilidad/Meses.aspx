<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Meses.aspx.cs" Inherits="XUSS.WEB.Contabilidad.Meses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_meses" runat="server" PageSize="1" AllowPaging="True"  
            OnItemCommand="rlv_mes_OnItemCommand" OnItemDataBound="rlv_mes_OnItemDataBound" 
            DataSourceID="obj_anos" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Maestro Mes/Año</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_meses" PageSize="1" RenderMode="Lightweight">
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
                                    Maestro Mes/Año
                                </h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">                                                    
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar"  />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />                    
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>Año</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ano" runat="server" Enabled="false" Text='<%# Bind("MA_ANO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                        Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                        <MasterTableView ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="MA_MES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Cod Mes" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MA_MES"
                                                UniqueName="MA_MES">
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOM_MES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOM_MES"
                                                UniqueName="NOM_MES">
                                            </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                 <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>Año</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ano" runat="server" Enabled="true" Text='<%# Bind("MA_ANO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_ano" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" >
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_NeedDataSource"
                        Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                        <MasterTableView ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="MA_MES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Cod Mes" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MA_MES"
                                                UniqueName="MA_MES">
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOM_MES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOM_MES"
                                                UniqueName="NOM_MES">
                                            </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                     <table>
                         <tr>
                             <td>
                                 <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                             </td>
                         </tr>
                     </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>

    <asp:ObjectDataSource ID="obj_anos" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_anos_Inserting"
        SelectMethod="GetAnos" TypeName="XUSS.BLL.Parametros.MesesBL" InsertMethod="InsertMeses">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="MA_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="MA_ANO" Type="Int32" />
            <asp:SessionParameter Name="MA_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbMes" Type="Object" />            
        </InsertParameters>
    </asp:ObjectDataSource>

</asp:Content>
