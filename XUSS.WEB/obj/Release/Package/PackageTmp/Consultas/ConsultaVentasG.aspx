<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaVentasG.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaVentasG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
             />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Ventas x Almacen</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
                <tr>
                    <td><label>Fecha</label></td>
                    <td>                     
                        <telerik:RadDatePicker ID="edt_fecha" runat="server">
                        </telerik:RadDatePicker>                
                    </td>                    
                    <td>
                    <telerik:RadButton ID="btn_cargar" runat="server" Text="Refrescar" 
                            onclick="btn_cargar_Click">
                    </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>            
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="950px" AutoGenerateColumns="False"
                Culture="(Default)" CellSpacing="0" 
                DataSourceID="obj_consulta" ShowFooter="true">
                <MasterTableView ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridTemplateColumn DataField="CHK" HeaderText=" " UniqueName="cestado"
                            HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox ID="estadoLabel" runat="server" Checked='<%# GetCheck(Eval("CHK"))%>' Enabled="false"
                                     />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Almacen" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SUB" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="Sub. Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SUB" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IVA" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="IVA" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IVA" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TOT" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TOT" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EFECTIVO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" Aggregate="Sum"
                            HeaderText="vlr Efectivo" ItemStyle-HorizontalAlign="Right" Resizable="true" DataFormatString="{0:N}"
                            SortExpression="EFECTIVO">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="CHEQUED" HeaderButtonType="TextButton" HeaderStyle-Width="100px" Aggregate="Sum"
                            HeaderText="vlr Cheques Dia" ItemStyle-HorizontalAlign="Right" Resizable="true" DataFormatString="{0:N}"
                            SortExpression="CHEQUED">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="TDB" HeaderButtonType="TextButton" HeaderStyle-Width="80px" Aggregate="Sum"
                            HeaderText="vlr T. Debito" ItemStyle-HorizontalAlign="Right" Resizable="true" DataFormatString="{0:N}"
                            SortExpression="TDB">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TCD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" Aggregate="Sum"
                            HeaderText="vlr T. Credito" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TCD" DataFormatString="{0:N}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="CHEQUEP" HeaderButtonType="TextButton" HeaderStyle-Width="300px" DataFormatString="{0:N}"
                            HeaderText="vlr Cheque Pos." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CHEQUEP" Aggregate="Sum">
                            <HeaderStyle Width="300px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="GIFTCARD" HeaderButtonType="TextButton" HeaderStyle-Width="100px" Aggregate="Sum"
                            HeaderText="Gift Card" ItemStyle-HorizontalAlign="Right" Resizable="true" DataFormatString="{0:N}"
                            SortExpression="GIFTCARD">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OTROS" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="vlr Otros" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTROS" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
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
                </MasterTableView>                
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <ClientSettings>                                       
                    <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="470px"/>
                </ClientSettings>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                </HeaderContextMenu>
            </telerik:RadGrid>

        </div>
        </telerik:RadAjaxPanel>
        <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetVentas" TypeName="XUSS.BLL.Consultas.ConsultaVentasGBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:ControlParameter ControlID="edt_fecha" DefaultValue="" Name="filter" 
                PropertyName="SelectedDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>        

</asp:Content>
