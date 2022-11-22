<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadFileBinary.ascx.cs" Inherits="XUSS.WEB.ControlesUsuario.LoadFileBinary" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnBuscar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnBuscar" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadListView1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="RadGrid3" UpdatePanelHeight="" />                  
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnNuevo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnNuevo" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rauImagen">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Thumbnail" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="rauImagen" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Button1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Button1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadListView1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="RadGrid3" UpdatePanelHeight="" />                                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadListView1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadListView1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="txtTexto" UpdatePanelHeight="" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadGrid2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="txtTexto" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="raeHTMLVer" UpdatePanelHeight="" />                                                
            </UpdatedControls>
        </telerik:AjaxSetting>        
        <telerik:AjaxSetting AjaxControlID="rauHTML">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rdeHTML" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="rauHTML" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rauTexto">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rtbTexto" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="rauTexto" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>    
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="txtTexto" UpdatePanelHeight="" />                
                <telerik:AjaxUpdatedControl ControlID="rtbVerTexto" UpdatePanelHeight="" />                                                
            </UpdatedControls>
        </telerik:AjaxSetting>    
        <telerik:AjaxSetting AjaxControlID="RadGrid3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid3" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="txtTexto" UpdatePanelHeight="" />                
            </UpdatedControls>
        </telerik:AjaxSetting>            
        <telerik:AjaxSetting AjaxControlID="btnAgregar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtTexto" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="btnAgregar" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />                                
            </UpdatedControls>
        </telerik:AjaxSetting>                
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<style type="text/css">
    .myClass
    {
        text-decoration:none;
        color:#000000 !important;
    }
    .myClass:hover
    {
        background-color: #a1da29 !important;
    }        
</style> 
<table  class="tablePopul" style="padding: 0px !important;">
    <tr>
        <td><telerik:RadTextBox ID="txtTexto" runat="server"  ReadOnly="true" Enabled="false" /></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="..." Enabled="false"
                onclick="btnBuscar_Click" CausesValidation="false" /></td>
        <td><asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                onclick="btnNuevo_Click" CausesValidation="false" Enabled="false"/></td>
    </tr>
</table>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlNuevo" TargetControlID="Button23"  
    CancelControlID="btnCancelar" BackgroundCssClass="modalBackground"  />    
<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlBuscar" TargetControlID="Button34"  
    CancelControlID="btnCancelarBusqueda" BackgroundCssClass="modalBackground" /> 
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlVerTexto"   
     BackgroundCssClass="modalBackgroundTexto" TargetControlID="Button2"  CancelControlID="btnCancelarVerTexto"/> 
<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlVerHTML"   
     BackgroundCssClass="modalBackgroundTexto" TargetControlID="Button22"  CancelControlID="btnCancelarVerHTML"/> 
<asp:Panel ID="pnlNuevo" runat="server"  CssClass="modalPopup" Width="550px" Style="display: none;">
    <fieldset class="cssFieldSet21" style="width:auto;">
        <legend>Nuevo</legend>
        <div style="padding:20px;">
            <table width="505px"  class="tablePopul"  >
                <tr>
                    <td width="50px">Nombre</td>
                    <td width="445px">
                        <telerik:RadTextBox ID="rtbNombre" Runat="server" Width="440px"/>           
                    </td>        
                </tr>
            </table>
            <asp:MultiView  ID="MultiView1" runat="server">        
                <asp:View ID="Imagen" runat="server"> 
                    <table width="505px" class="tablePopul" >
                        <tr>
                            <td width="50px">Imagen</td>
                            <td width="445px">
                                <telerik:RadBinaryImage runat="server" Width="200px" Height="150px"
                                    ResizeMode="Fit"  ID="Thumbnail" Visible="False" />
                                <telerik:RadProgressManager ID="RadProgressManager1" Runat="server" />
                                <telerik:RadAsyncUpload ID="rauImagen" runat="server" 
                                    MaxFileInputsCount="1" onfileuploaded="RadAsyncUpload1_FileUploaded"                                                                                                               
                                    InputSize="23" Width="390px"  OverwriteExistingFiles="True">
                                </telerik:RadAsyncUpload>
                                <telerik:RadProgressArea ID="RadProgressArea1" Runat="server" DisplayCancelButton="True" 
                                    ProgressIndicators="TotalProgressBar, TotalProgress, TotalProgressPercent, RequestSize, CurrentFileName, TimeElapsed, TimeEstimated, TransferSpeed" >            
                                    <Localization Uploaded="Uploaded"></Localization>
                                </telerik:RadProgressArea>                    
                            </td>
                        </tr>                    
                    </table>                    
                </asp:View>
                <asp:View ID="Texto" runat="server">
                    <table width="505px" class="tablePopul" >
                        <tr>
                            <td width="50px" rowspan="2">Texto</td>
                            <td width="445px">              
                                <telerik:RadTextBox ID="rtbTexto" Runat="server" Height="112px" 
                                    TextMode="MultiLine" Width="440px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>    
                        <tr>
                            <td>
                                <telerik:RadProgressManager ID="RadProgressManager2" Runat="server" />
                                <telerik:RadAsyncUpload ID="rauTexto" runat="server" 
                                    MaxFileInputsCount="1" InputSize="23" Width="390px" 
                                    onfileuploaded="rauTexto_FileUploaded">
                                </telerik:RadAsyncUpload>
                                <telerik:RadProgressArea ID="RadProgressArea2" Runat="server" DisplayCancelButton="True" 
                                    ProgressIndicators="TotalProgressBar, TotalProgress, TotalProgressPercent, RequestSize, CurrentFileName, TimeElapsed, TimeEstimated, TransferSpeed" >            
                                    <Localization Uploaded="Uploaded"></Localization>
                                </telerik:RadProgressArea>
                            </td>
                        </tr>                
                    </table> 
                </asp:View>
                <asp:View ID="HTML" runat="server">
                    <table width="505px" class="tablePopul" >
                        <tr>
                            <td width="50px" rowspan="2">Texto</td>
                            <td width="445px">              
                                <telerik:RadEditor ID="rdeHTML" Runat="server" Width="440px">
                                    <Tools>
                                    <telerik:EditorToolGroup Tag="InsertToolbar">
                                            <telerik:EditorTool Name="AjaxSpellCheck" />
                                            <telerik:EditorTool Name="ImageMapDialog" />
                                            <telerik:EditorTool Name="InsertTable" />
                                            <telerik:EditorTool Name="InsertRowAbove" />
                                            <telerik:EditorTool Name="InsertRowBelow" />
                                            <telerik:EditorTool Name="DeleteRow" />
                                            <telerik:EditorTool Name="InsertColumnLeft" />
                                            <telerik:EditorTool Name="InsertColumnRight" />
                                            <telerik:EditorTool Name="DeleteColumn" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="MergeColumns" />
                                            <telerik:EditorTool Name="MergeRows" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="SplitCell" />
                                            <telerik:EditorTool Name="SplitCellHorizontal" />
                                            <telerik:EditorTool Name="SetCellProperties" />
                                            <telerik:EditorTool Name="SetTableProperties" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool Name="Undo" />
                                            <telerik:EditorTool Name="Redo" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="Cut" />
                                            <telerik:EditorTool Name="Copy" />
                                            <telerik:EditorTool Name="Paste" ShortCut="CTRL+!" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="PasteFromWord" />
                                            <telerik:EditorTool Name="PasteFromWordNoFontsNoSizes" />
                                            <telerik:EditorTool Name="PastePlainText" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="FindAndReplace" />
                                            <telerik:EditorTool Name="SelectAll" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="InsertGroupbox" />
                                            <telerik:EditorTool Name="InsertParagraph" />
                                            <telerik:EditorTool Name="InsertHorizontalRule" />
                                            <telerik:EditorSeparator />                
                                            <telerik:EditorTool Name="PageProperties" />
                                            <telerik:EditorTool Name="FormatCodeBlock" />           
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool Name="Bold" />
                                            <telerik:EditorTool Name="Italic" />
                                            <telerik:EditorTool Name="Underline" />
                                            <telerik:EditorTool Name="StrikeThrough" />
                                        <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="JustifyLeft" />
                                            <telerik:EditorTool Name="JustifyCenter" />
                                            <telerik:EditorTool Name="JustifyRight" />
                                            <telerik:EditorTool Name="JustifyFull" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="Superscript" />
                                            <telerik:EditorTool Name="Subscript" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="ConvertToLower" />
                                            <telerik:EditorTool Name="ConvertToUpper" />
                                            <telerik:EditorTool Name="Indent" />
                                            <telerik:EditorTool Name="Outdent" />
                                            <telerik:EditorTool Name="InsertOrderedList" />
                                            <telerik:EditorTool Name="InsertUnorderedList" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup Tag="DropdownToolbar">
                                            <telerik:EditorTool Name="ForeColor" />
                                            <telerik:EditorTool Name="BackColor" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="FontName" />
                                            <telerik:EditorTool Name="RealFontSize" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="InsertSymbol" />
                                            <telerik:EditorTool Name="FormatStripper" />
                                            <telerik:EditorTool Name="LinkManager" />
                                            <telerik:EditorTool Name="Unlink" />   
                                            <telerik:EditorTool Name="InsertImage" />                             
                                        </telerik:EditorToolGroup>
                                    </Tools>
                                    <Content>
                                    </Content>
                                </telerik:RadEditor>
                            </td>
                        </tr>    
                        <tr>
                            <td>
                                <telerik:RadProgressManager ID="RadProgressManager3" Runat="server" />
                                <telerik:RadAsyncUpload ID="rauHTML" runat="server" 
                                    MaxFileInputsCount="1" InputSize="23" Width="390px" 
                                    onfileuploaded="rauHTML_FileUploaded">
                                </telerik:RadAsyncUpload>
                                <telerik:RadProgressArea ID="RadProgressArea3" Runat="server" DisplayCancelButton="True" 
                                    ProgressIndicators="TotalProgressBar, TotalProgress, TotalProgressPercent, RequestSize, CurrentFileName, TimeElapsed, TimeEstimated, TransferSpeed" >            
                                    <Localization Uploaded="Uploaded"></Localization>
                                </telerik:RadProgressArea>
                            </td>
                        </tr>
                   </table>                
                </asp:View>
                <asp:View ID="Binario" runat="server">
                    <table width="505px" class="tablePopul" >
                        <tr>
                            <td width="50px">Binario</td>
                            <td width="445px">
                                <telerik:RadProgressManager ID="RadProgressManager4" Runat="server" />
                                <telerik:RadAsyncUpload ID="rauBinario" runat="server" 
                                    MaxFileInputsCount="1" 
                                    InputSize="23" Width="390px">
                                </telerik:RadAsyncUpload>
                                <telerik:RadProgressArea ID="RadProgressArea4" Runat="server" DisplayCancelButton="True" 
                                    ProgressIndicators="TotalProgressBar, TotalProgress, TotalProgressPercent, RequestSize, CurrentFileName, TimeElapsed, TimeEstimated, TransferSpeed" >            
                                    <Localization Uploaded="Uploaded"></Localization>
                                </telerik:RadProgressArea>                    
                            </td>
                        </tr>                    
                    </table>                                
                </asp:View>
            </asp:MultiView>        
        </div>
    </fieldset>
    <table class="tablePopul" width="545px" style="text-align:center;">            
        <tr>
            <td align="center" width="545px" colspan="2"  ><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></td>
        </tr>    
        <tr>
            <td align="right">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" onclick="btnAgregar_Click" CausesValidation="false"/>
            </td>
            <td align="left">
                <asp:Button ID="btnCancelar" runat="server"  Text="Cancelar" CausesValidation="false" />
            </td>
        </tr>
    </table>    
</asp:Panel>
<asp:Panel ID="pnlBuscar" runat="server"  CssClass="modalPopup" Width="502px" Style="display: none;">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetByIdTipo" 
        TypeName="AseingesOut.SCW.BLL.Administracion.AdmiBlobBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
            <asp:ControlParameter DefaultValue="" Name="idTipo" ControlID="MultiView1"  PropertyName="ActiveViewIndex" Type="Int32" />        
        </SelectParameters>
    </asp:ObjectDataSource>
    <fieldset class="cssFieldSet21" style="width:auto;">
        <legend>Buscar</legend>
        <table class="tablePopul" Width="500px"> 
            <tr>
                <td>Id:</td>
                <td>
                    <telerik:RadNumericTextBox ID="rnbIdBlob" Runat="server">
                    </telerik:RadNumericTextBox>
                </td>
                <td>Nombre</td>
                <td>
                    <telerik:RadTextBox ID="rtbBuscar" Runat="server">
                    </telerik:RadTextBox>
                </td> 
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Buscar" 
                        onclick="Button1_Click1" CausesValidation="false"/>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">        
            <asp:View ID="Imagenes" runat="server">  
                    <telerik:RadListView ID="RadListView1" runat="server" AllowPaging="True" 
                        DataKeyNames="BlobBlob" 
                        onselectedindexchanged="RadListView1_SelectedIndexChanged" >
                        <ItemTemplate>
                            <asp:LinkButton ID="sa" runat="server" class="myClass" CommandName="Select" 
                                style="float: left; margin: 5px 5px 5px 5px; padding: 2px 2px 2px 2px; background: #eeeeee;" 
                                Width="145px" CausesValidation="false">
                                <table border="0" class="tablePopul" style="padding: 5px !important;">
                                    <tr>
                                        <td>Nombre</td>
                                        <td><asp:Label ID="BlobNombreLabel" runat="server" 
                                                Text='<%# Eval("BlobNombre") %>' /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadBinaryImage ID="RadBinaryImage2" runat="server" 
                                            DataValue='<%# Eval("BlobBinario") %>' 
                                            Width="127px" Height="100px" ResizeMode="Fit"  />
                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="RadListView RadListView_Default">
                                <div class="rlvEmpty">
                                    No hay imagenes.
                                </div>
                            </div>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <div class="RadListView RadListViewFloated RadListView_Default">
                                <div class="rlvFloated">
                                    <div ID="itemPlaceholder" runat="server">
                                    </div>
                                </div>
                                <div style="text-align:center;">                                                                     
                                    <telerik:RadDataPager ID="RadDataPa15ger1" runat="server" 
                                        PagedControlID="RadListView1" PageSize="6"  >
                                        <Fields>
                                            <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                            <telerik:RadDataPagerButtonField FieldType="Numeric" />
                                            <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                        </Fields>
                                    </telerik:RadDataPager>
                                </div>
                            </div>
                        </LayoutTemplate>
                    </telerik:RadListView>
            </asp:View>
            <asp:View ID="TextoBuscar" runat="server">                
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
                    GridLines="None" Width="490px" 
                    CssClass="tablePopul" 
                    onselectedindexchanged="RadGrid1_SelectedIndexChanged" >                    
                    <MasterTableView AutoGenerateColumns="False"  CssClass="tablePopul" DataKeyNames="BlobBlob">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <PagerStyle Mode="NextPrev"   />                         
                        <Columns>
                            <telerik:GridTemplateColumn DataField="BlobBlob" DataType="System.Int32" 
                                HeaderText="Id" SortExpression="BlobBlob" UniqueName="BlobBlob">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbBlobBlob" runat="server" CommandName="Select" CssClass="myClass" CausesValidation="false"><asp:Label ID="BlobBlobLabel" runat="server" Text='<%# Eval("BlobBlob") %>' ></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobNombre" HeaderText="Nombre" 
                                SortExpression="BlobNombre" UniqueName="BlobNombre">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbBlobNombre" runat="server" CommandName="Select" CssClass="myClass" CausesValidation="false"><asp:Label ID="BlobNombreLabel" runat="server" Text='<%# Eval("BlobNombre") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobDescripcion" 
                                HeaderText="Descripcion" SortExpression="BlobDescripcion" 
                                UniqueName="BlobDescripcion">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobDescripcion" runat="server" OnClick="VerMas"  CssClass="myClass">Ver Texto</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:View>
            <asp:View ID="HTMLBuscar" runat="server">                
                <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" 
                    GridLines="None" Width="490px" 
                    CssClass="tablePopul" 
                    onselectedindexchanged="RadGrid1_SelectedIndexChanged">                    
                    <MasterTableView AutoGenerateColumns="False"  CssClass="tablePopul" DataKeyNames="BlobBlob">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <PagerStyle Mode="NextPrev"   />                          
                        <Columns>
                            <telerik:GridTemplateColumn DataField="BlobBlob" DataType="System.Int32" 
                                HeaderText="Id" SortExpression="BlobBlob" UniqueName="BlobBlob">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobBlob" runat="server" CommandName="Select" CssClass="myClass"><asp:Label ID="BlobBlobLabel" runat="server" Text='<%# Eval("BlobBlob") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobNombre" HeaderText="Nombre" 
                                SortExpression="BlobNombre" UniqueName="BlobNombre">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobNombre" runat="server" CommandName="Select" CssClass="myClass"><asp:Label ID="BlobNombreLabel" runat="server" Text='<%# Eval("BlobNombre") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobDescripcion" 
                                HeaderText="Descripcion" SortExpression="BlobDescripcion" 
                                UniqueName="BlobDescripcion">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobDescripcion" runat="server" OnClick="VerMasHTML"  CssClass="myClass">Ver Texto</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:View>            
            <asp:View ID="BuscarBinario" runat="server">                
                <telerik:RadGrid ID="RadGrid3" runat="server" AllowPaging="True" 
                    GridLines="None" Width="490px" 
                    CssClass="tablePopul" 
                    onselectedindexchanged="RadGrid1_SelectedIndexChanged">                    
                    <MasterTableView AutoGenerateColumns="False"  CssClass="tablePopul" DataKeyNames="BlobBlob">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <PagerStyle Mode="NextPrev"   />                          
                        <Columns>
                            <telerik:GridTemplateColumn DataField="BlobBlob" DataType="System.Int32" 
                                HeaderText="Id" SortExpression="BlobBlob" UniqueName="BlobBlob">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobBlob" runat="server" CommandName="Select" CssClass="myClass"><asp:Label ID="BlobBlobLabel" runat="server" Text='<%# Eval("BlobBlob") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobNombre" HeaderText="Nombre" 
                                SortExpression="BlobNombre" UniqueName="BlobNombre">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobNombre" runat="server" CommandName="Select" CssClass="myClass"><asp:Label ID="BlobNombreLabel" runat="server" Text='<%# Eval("BlobNombre") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobDescripcion" 
                                HeaderText="Descripcion" SortExpression="BlobDescripcion" 
                                UniqueName="BlobDescripcion">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" ID="lkbBlobDescripcion" runat="server" CommandName="Select"  CssClass="myClass"><asp:Label ID="BlobDescripcionLabel" runat="server" Text='<%# Eval("BlobDescripcion") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BlobOrigen" 
                                HeaderText="Archivo Origen" SortExpression="BlobOrigen" 
                                UniqueName="BlobOrigen">
                                <ItemTemplate>
                                    <asp:LinkButton CausesValidation="false"  ID="lkbBlobOrigen" runat="server" CommandName="Select" CssClass="myClass"><asp:Label ID="BlobOrigenLabel" runat="server" Text='<%# Eval("BlobOrigen") %>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:View>                        
        </asp:MultiView>        
    </fieldset>        
    <div style="text-align:center;">
        <asp:Button ID="btnCancelarBusqueda" runat="server" Text="Cancelar" CausesValidation="false" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlVerTexto" runat="server"  CssClass="modalPopupTexto" Width="502px" Style="display: none;">
    <div style="z-index: 5000; position: absolute; top:4px;left:484px; background-color:#ffffff;"><asp:ImageButton CausesValidation="false" ID="btnCancelarVerTexto" runat="server" CommandName="Delete" SkinID="SkinCancelUC" Text="Cerrar" Width="20px" Height="20px" /></div>
    <fieldset class="cssFieldSet21" style="width:auto;">
        <legend>Contenido</legend>
        <table  width="500px">
            <tr>
                <td>
                    <telerik:RadTextBox ID="rtbVerTexto" runat="server" TextMode="MultiLine" 
                        Width="450px" Height="400px" ReadOnly="true" >
                    </telerik:RadTextBox>                                        
                </td>    
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<asp:Panel ID="pnlVerHTML" runat="server"  CssClass="modalPopupTexto" Width="502px" Style="display: none;">
    <div style="z-index: 5000; position: absolute; top:4px;left:484px; background-color:#ffffff;"><asp:ImageButton CausesValidation="false" ID="btnCancelarVerHTML" runat="server" CommandName="Delete" SkinID="SkinCancelUC" Text="Cerrar" Width="20px" Height="20px" /></div>
    <fieldset class="cssFieldSet21" style="width:auto;">
        <legend>Contenido</legend>
        <table  width="500px">
            <tr>
                <td>
                    <telerik:RadEditor ID="raeHTMLVer" Runat="server" Width="445px" Height="400px">                
                        <Content></Content>
                    </telerik:RadEditor>
                </td>     
            </tr>
        </table>
     </fieldset>        
</asp:Panel>
<div style="display:none;">
    <asp:Button ID="Button2" runat="server" Text="Button"  CausesValidation="false" />
    <asp:Button ID="Button22" runat="server" Text="Button" CausesValidation="false" />    
    <asp:Button ID="Button23" runat="server" Text="Button" CausesValidation="false" />        
    <asp:Button ID="Button34" runat="server" Text="Button" CausesValidation="false" />            
</div>
