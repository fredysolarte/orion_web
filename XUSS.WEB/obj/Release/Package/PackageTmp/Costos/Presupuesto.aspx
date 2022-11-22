<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Presupuesto.aspx.cs" Inherits="XUSS.WEB.Costos.Presupuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
             />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Presupuesto x Tienda</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
                <tr>
                    <td>
                        <label>
                            Mes</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_mes" runat="server" AppendDataBoundItems="true">
                            <Items>                                
                                <telerik:RadComboBoxItem Value="1" Text="Enero" />
                                <telerik:RadComboBoxItem Value="2" Text="Febrero" />
                                <telerik:RadComboBoxItem Value="3" Text="Marzo" />
                                <telerik:RadComboBoxItem Value="4" Text="Abril" />
                                <telerik:RadComboBoxItem Value="5" Text="Mayo" />
                                <telerik:RadComboBoxItem Value="6" Text="Junio" />
                                <telerik:RadComboBoxItem Value="7" Text="Julio" />
                                <telerik:RadComboBoxItem Value="8" Text="Agosto" />
                                <telerik:RadComboBoxItem Value="9" Text="septiembre" />
                                <telerik:RadComboBoxItem Value="10" Text="Octubre" />
                                <telerik:RadComboBoxItem Value="11" Text="Noviembre" />
                                <telerik:RadComboBoxItem Value="12" Text="Diciembre" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <label>
                            Año</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_ano" runat="server" AppendDataBoundItems="true">
                            <Items>                                                                
                                <telerik:RadComboBoxItem Value="2016" Text="2016" />
                                <telerik:RadComboBoxItem Value="2017" Text="2017" />
                                <telerik:RadComboBoxItem Value="2018" Text="2018" />
                                <telerik:RadComboBoxItem Value="2019" Text="2019" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <label>
                            Almacen</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-ES" DataSourceID="obj_bodegas"
                            DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true" Width="300px">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="Seleccionar"/>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadButton ID="btn_consultar" runat="server" Text="Consultar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_consultar_Click">
                        </telerik:RadButton>
                    </td>                    
                    <td>
                        <telerik:RadButton ID="btn_guardar" runat="server" Text="Guardar" Icon-PrimaryIconCssClass="rbAdd" onclick="btn_guardar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                 Culture="(Default)" CellSpacing="0" 
                ShowFooter="true">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="bodega" HeaderText=" " UniqueName="cestado"
                            HeaderStyle-Width="300px">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-ES" SelectedValue='<%# Eval("bodega") %>'
                                    DataSourceID="obj_bodegas" DataTextField="BDNOMBRE" DataValueField="BDBODEGA"
                                    Enabled="false" Width="290px">
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ano" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Año" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ano">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mes" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Mes" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="mes">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dia" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Dia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="dia">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                                                
                        <%--<telerik:GridBoundColumn DataField="valor" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                DataFormatString="{0:C}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="valor" UniqueName="valor" FooterText="Total: "
                                                Aggregate="Sum">                                                
                                            </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn DataField="valor" HeaderText="Valor" UniqueName="valor" 
                            HeaderStyle-Width="150px" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="{0:C}" >
                            <ItemTemplate>                            
                                <telerik:RadNumericTextBox ID="edt_cantidad" runat="server" value='<%# GetValor(Eval("valor")) %>'
                                    Width="150px" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>                            
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    <%--</telerik:RadAjaxPanel>--%>
    <asp:ObjectDataSource ID="obj_bodegas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLstBodegas" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="ALMACEN" Type="String" DefaultValue="S" />
        </SelectParameters>
    </asp:ObjectDataSource>
   <%-- <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPresupuesto" TypeName="XUSS.BLL.Costos.PresupuestoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:ControlParameter ControlID="rc_mes" DefaultValue="" Name="inmes" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="rc_ano" DefaultValue="" Name="inano" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="rc_bodega" DefaultValue="" Name="inbodega" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
     
</asp:Content>
