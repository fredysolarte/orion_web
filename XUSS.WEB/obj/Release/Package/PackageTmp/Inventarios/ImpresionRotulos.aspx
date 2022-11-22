<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ImpresionRotulos.aspx.cs" Inherits="XUSS.WEB.Inventarios.ImpresionRotulos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" >
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnMaestro" runat="server" >
                <div class="box">
                    <div class="title">
                        <h5>Impresion Rotulos</h5>
                    </div>
                </div>
                <table>
                    <tr>
                        <td>
                            <label>Barras</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_codbarras" runat="server" Enabled="true" Visible="true" OnTextChanged="txt_codbarras_TextChanged" AutoPostBack="true">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>

                        <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Visible="false">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Visible="false">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Visible="false">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Visible="false">
                        </telerik:RadTextBox>                        
                        <td>
                            <label>Referencia</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Visible="true">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_referencia" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Talla</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nomc2" runat="server" Enabled="true" Visible="true">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nomc2" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Color</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nomc3" runat="server" Enabled="true" Visible="true">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_nomc3" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Producto</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Visible="true" Width="450px">
                            </telerik:RadTextBox>
                            <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Cantidad</label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txt_cantidad" runat="server" Enabled="true" NumberFormat-DecimalDigits="0">
                                </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_cantidad" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_cantidad" InitialValue="0"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_masivo" runat="server" Text="Agregar" OnClick="btn_masivo_Click" Icon-PrimaryIconCssClass="rbOpen" ToolTip="Agregar"  />
                        </td>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" OnClick="btn_agregar_OnClick" Icon-PrimaryIconCssClass="rbAdd"  ToolTip="Agregar" ValidationGroup="gvInsert" />
                        </td>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint"  ToolTip="Imprimir"  />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnl_detalle">
                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="350px" OnDeleteCommand ="rg_items_OnDeleteCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                                     <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"  ></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" DataKeyNames="ITM">
                                        <Columns>    
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="C1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C1"
                                                UniqueName="C1">                                                
                                            </telerik:GridBoundColumn>                                        
                                            <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMBRE"
                                                UniqueName="NOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NC2"
                                                UniqueName="NC2">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NC3"
                                                UniqueName="NC3">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CAN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="CAN" UniqueName="CAN" >                                                
                                            </telerik:GridBoundColumn>                                            
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                            <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
            </asp:Panel>
        </fieldset>        
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpArticulos" runat="server" Width="900px" Height="460px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" style="z-index: 1900;" >
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Referencia</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_referencia" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nombreart" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel2" runat="server">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%" AllowFilteringByColumn="True"  AllowSorting="true" 
                                AutoGenerateColumns="False" AllowPaging="true" PageSize="25"  CellSpacing="0" GridLines="None"
                                DataSourceID="obj_articulos" OnItemCommand="rgConsultaArticulos_OnItemCommand">
                                <FilterMenu Style="z-index: 2001;"></FilterMenu>
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Sel" CommandName="Select">
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARTIPPRO" HeaderText="" Visible="true" AllowFiltering="false"
                                            UniqueName="ARTIPPRO" HeaderButtonType="None" DataField="ARTIPPRO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE1" HeaderText="Referencia"
                                            UniqueName="ARCLAVE1" HeaderButtonType="TextButton" DataField="ARCLAVE1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE2" HeaderText="Propietario" UniqueName="ARCLAVE2"
                                            HeaderStyle-Width="180px" AllowFiltering="false" SortExpression="ARCLAVE2" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave2" runat="server" Text='<%# Eval("ARCLAVE2") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="C2" Visible="true"
                                            UniqueName="CLAVE2" HeaderButtonType="None" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE3" HeaderText="Propietario" UniqueName="ARCLAVE3"
                                            HeaderStyle-Width="180px" AllowFiltering="false" SortExpression="ARCLAVE3" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave3" runat="server" Text='<%# Eval("ARCLAVE3") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="C3" Visible="true" 
                                            UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="60px" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE4" HeaderText="" Visible="true"
                                            UniqueName="ARCLAVE4" HeaderButtonType="None" DataField="ARCLAVE4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
                                            UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="400px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC4" HeaderText="" AllowFiltering="false"
                                            UniqueName="ARDTTEC4" HeaderButtonType="TextButton" DataField="ARDTTEC4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC1" HeaderText="" AllowFiltering="false"
                                            UniqueName="ARDTTEC1" HeaderButtonType="TextButton" DataField="ARDTTEC1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn Resizable="true" SortExpression="PRECIO" HeaderText="Precio Lts" DataFormatString="{0:0.0}"
                                            UniqueName="PRECIO" HeaderButtonType="TextButton" DataField="PRECIO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="DESCUENTO" HeaderText="Dcto" DataFormatString="{0:0.#}"
                                            UniqueName="DESCUENTO" HeaderButtonType="TextButton" DataField="DESCUENTO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Can" DataFormatString="{0:0.#}"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>--%>
                                    </Columns>
                                    <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                        <strong>Alerta!</strong>No Tiene Registros
                                    </div>
                                </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Upload Archivo">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4</asp:ListItem>
                                        <asp:ListItem>Cod. Barras</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Aceptar" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_articulos" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetArticulos" TypeName="XUSS.BLL.Articulos.ArticulosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" AND 1=0" Name="filter" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
