<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Polla.aspx.cs" Inherits="XUSS.WEB.Tareas.Polla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:Panel runat="server" ID="pnlPrincipal">
         <fieldset class="cssFieldSetContainer">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Grupo A">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Grupo B">
                        </telerik:RadTab>              
                        <telerik:RadTab Text="Grupo C">
                        </telerik:RadTab>              
                        <telerik:RadTab Text="Grupo D">
                        </telerik:RadTab>              
                        <telerik:RadTab Text="Grupo E">
                        </telerik:RadTab>              
                        <telerik:RadTab Text="Grupo F">
                        </telerik:RadTab>             
                        <telerik:RadTab Text="Grupo G">
                        </telerik:RadTab>              
                        <telerik:RadTab Text="Grupo H">
                        </telerik:RadTab>                        
                    </Tabs>
                </telerik:RadTabStrip>    
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupoa">
                                <telerik:RadGrid ID="rgGrupoA" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoA">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupob">
                                <telerik:RadGrid ID="grGrupoB" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoB">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupoC">
                                <telerik:RadGrid ID="rgGrupoC" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoC">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupod">
                                <telerik:RadGrid ID="rgGrupoD" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoD">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView5" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_gropoe">
                                <telerik:RadGrid ID="rgGrupoE" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoE">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView6" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupof">
                                <telerik:RadGrid ID="rgGrupoF" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoF">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView7" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupog">
                                <telerik:RadGrid ID="rgGrupoG" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoG">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView8" runat="server">
                        <div>
                            <asp:Panel runat="server" ID="pnl_grupoh">
                                <telerik:RadGrid ID="rgGrupoH" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                     Culture="(Default)" CellSpacing="0" DataSourceID="obj_grupoH">
                                    <MasterTableView>
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="gPartidos" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="gResultados" HeaderText="Resultados" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Font-Size="9px">
                                            </telerik:GridColumnGroup>                                            
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PR_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Partido" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PR_CODIGO">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="E_LOCAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="E_LOCAL">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_LMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_LMARCADOR" UniqueName="PR_LMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_VMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gPartidos"
                                                Resizable="true" SortExpression="PR_VMARCADOR" UniqueName="PR_VMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="E_VISITANTE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ColumnGroupName="gPartidos"
                                                AllowFiltering="false" HeaderText="Equipo" ItemStyle-HorizontalAlign="Left"
                                                Resizable="true" SortExpression="E_VISITANTE">                                                                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RLMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RLMARCADOR" UniqueName="PR_RLMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_RVMARCADOR" HeaderStyle-Width="40px" HeaderText="" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_RVMARCADOR" UniqueName="PR_RVMARCADOR">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="40px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PR_PUNTOS" HeaderStyle-Width="60px" HeaderText="Puntos" ColumnGroupName="gResultados"
                                                Resizable="true" SortExpression="PR_PUNTOS" UniqueName="PR_PUNTOS">
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="edt_lmarcador" runat="server" Width="60px">
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
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
        </fieldset>
    </asp:Panel>
    <asp:ObjectDataSource ID="obj_grupoA" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="1,2,17,18,33,34"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoB" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="3,4,19,20,35,36"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoC" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="5,6,21,22,39,40"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoD" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="7,8,23,24,39,40"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoE" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="9,10,25,26,41,42"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoF" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="11,12,27,28,43,44"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoG" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="13,14,29,30,45,46"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grupoH" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetGrupo" TypeName="XUSS.BLL.Tareas.PollaBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inCadena" Type="String" DefaultValue="15,16,31,32,47,48"/>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
