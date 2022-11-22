<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="InterfacesCargue.aspx.cs" Inherits="XUSS.WEB.Parametros.InterfacesCargue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .RadAsyncUpload, .RadAsyncUpload * {
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
        }
    </style>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Cargue Archivos</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnVacio" runat="server">
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>Tabla</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tablas" runat="server" DataSourceID="obj_tabla" Width="300px" OnSelectedIndexChanged="rc_tablas_SelectedIndexChanged"
                                    DataTextField="name" DataValueField="object_id" AppendDataBoundItems="true" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Archivo</label></td>
                            <td>
                                <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                            </td>
                            <td>
                                <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Aceptar" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </fieldset>
        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
            SelectedIndex="3" CssClass="tabStrip">
            <Tabs>
                <telerik:RadTab Text="Cargue" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab Text="Errores">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="pv_datos" runat="server">
                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                    <telerik:RadPane ID="LeftPane" runat="server" Width="50%">
                        <telerik:RadGrid ID="rg_campos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView ShowGroupFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="nom_campo" HeaderButtonType="TextButton" HeaderStyle-Width="130px"
                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="nom_campo"
                                        UniqueName="nom_campo">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="nom_tipo" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Tipo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="nom_tipo"
                                        UniqueName="nom_tipo">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="max_length" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Tamaño" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="max_length" UniqueName="max_length">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Vacio" UniqueName="clote" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_vacio" runat="server" Checked='<%# this.GetEstado(Eval("is_nullable")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Llave" UniqueName="ckey" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_pk" runat="server" Checked='<%# this.GetEstado(Eval("is_primary_key")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Llave" UniqueName="ckey" HeaderStyle-Width="300px">
                                        <ItemTemplate>
                                            <telerik:RadComboBox ID="rc_campos" runat="server" Width="300px" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                        <strong>Alerta!</strong>No Tiene Registros
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                    </telerik:RadSplitBar>
                    <telerik:RadPane ID="RadPane1" runat="server" Width="50%">
                        <telerik:RadGrid ID="rg_cargue" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView ShowGroupFooter="true">
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <telerik:RadGrid ID="rg_Errores" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView ShowGroupFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fila" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Fila" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="fila"
                                        UniqueName="fila">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Columna" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Columna" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="fila"
                                        UniqueName="Columna">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Campo" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Campo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="fila"
                                        UniqueName="Campo">                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Error" HeaderButtonType="TextButton" HeaderStyle-Width="380px"
                                        HeaderText="Error" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="fila"
                                        UniqueName="Error">                                        
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
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnl_next" runat="server">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_validar" runat="server" Text="Validar" ToolTip="Validar" Icon-PrimaryIconCssClass="rbRefresh" OnClick="btn_validar_Click" />
                        </td>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_siguiente" runat="server" Text="Cargar" ToolTip="Siguiente" Icon-PrimaryIconCssClass="rbUpload" OnClick="btn_siguiente_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_tabla" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTablas" TypeName="XUSS.BLL.Parametros.CarguePlanosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
