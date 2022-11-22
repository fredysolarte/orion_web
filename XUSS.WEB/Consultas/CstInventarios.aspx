<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CstInventarios.aspx.cs" Inherits="XUSS.WEB.Consultas.CstInventarios" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Default" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Disponibilidad de Inventarios</h5>
                </div>
            </div>
        </fieldset>    
    <div>
            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="SkinBuscarUC" OnClick="im_buscar" 
                ToolTip="Buscar" />
        </div>
    <div>
        <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="960px" AutoGenerateColumns="False"
            Culture="(Default)" CellSpacing="0" 
            DataSourceID="obj_consulta" AllowPaging="True" >
            <MasterTableView DataSourceID="obj_consulta">
                <Columns>
                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Referncia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="ARCLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Talla" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE2">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Color" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE3">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="300px"
                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE">
                        <HeaderStyle Width="300px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE">
                        <HeaderStyle Width="200px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="BBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BBCANTID">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                <telerik:GridTemplateColumn DataField="IM_IMAGEN" HeaderText="Foto"
										UniqueName="IM_IMAGEN">
										<ItemTemplate>
											<cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
												TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
											</cc1:ModalPopupExtender>
											<asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
												<fieldset class="cssFieldSetContainer" style="width: auto !important;">
													<div class="box">
														<div class="title">
															<h5>Fotografia </h5>
														</div>
													</div>
													<div style="padding: 5px 5px 5px 5px">
														<telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IM_IMAGEN") is DBNull ? null : Eval("IM_IMAGEN")%>'
									                    AutoAdjustImageControlSize="false" Width="550" Height="470px" ToolTip='<%#Eval("IM_IMAGEN", "Foto {0}") %>'
									                    AlternateText='<%#Eval("IM_IMAGEN", "Foto {0}") %>' />
														<div style="text-align: center;">
															<asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
														</div>
													</div>
												</fieldset>
											</asp:Panel>
											<asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
										</ItemTemplate>
										<HeaderStyle Width="100px"></HeaderStyle>
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
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
            </HeaderContextMenu>
        </telerik:RadGrid>

        <asp:ModalPopupExtender ID="ModalPopupConsulta" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnPopUpEdit" TargetControlID="Button3">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnPopupEdit" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 590px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Datos a Buscar</h5>
                    </div>
                </div>
                <div style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <label>Bodega</label>
                            </td>
                            <td>
                            <telerik:RadComboBox ID="rc_bodega" runat="server" DataSourceID="obj_bodega" Width="412px"
                                DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="BODEGA PRINCIPAL(CEDI)" Value="BO" />
                                </Items>
                            </telerik:RadComboBox>                            
                            <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetBodega" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
                                <SelectParameters>
                                    <asp:Parameter Name="connection" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Linea</label>
                            </td>
                            <td>
                            <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="Obj_Linea" Width="412px"
                                DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />                                    
                                </Items>
                            </telerik:RadComboBox>                            
                            <asp:ObjectDataSource ID="Obj_Linea" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetLinea" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
                                <SelectParameters>
                                    <asp:Parameter Name="connection" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Referencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_referencia" runat="server" Width="412px">
                                </telerik:RadTextBox>                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Talla</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_talla" runat="server" Width="412px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Color</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_color" runat="server" Width="412px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                    </table>
                </div>
            </fieldset>
            <div style="text-align: right;">
            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton" CausesValidation="true"></telerik:RadButton>
            <telerik:RadButton ID="bt_cerrar" runat="server" Text="Cerrar" OnClick="bt_cerrar_click"></telerik:RadButton>
        </div>
        </asp:Panel>
        
    </div>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="Get_Inventario" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>
