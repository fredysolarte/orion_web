<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ctl_Personas.ascx.cs"
	Inherits="XUSS.WEB.ControlesUsuario.Ctl_Personas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Panel ID="pnFinder" runat="server" CssClass="modalPopup" Style="display: none;
	width: 600px !important; z-index: 1003 !important;">
	<telerik:RadAjaxPanel ID="rapVisitas" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
	<fieldset class="cssFieldSetContainer" style="width: 590px !important">
		<div class="box">
			<div class="title">
				<h5>
					Buscar Personas</h5>
			</div>
		</div>
		<div style="width: 100%">
			<table>
				<tr>
					<td>
						<label>
							No. Identificacion</label>
					</td>
					<td>
						<telerik:RadTextBox ID="txt_identificacion" runat="server">
						</telerik:RadTextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							Nombre</label>
					</td>
					<td>
						<telerik:RadTextBox ID="txt_Nombre" runat="server">
						</telerik:RadTextBox>
					</td>
					<td>						
						<asp:ImageButton ID="btn_find" runat="server"  onclick="btn_find_Click" ImageUrl="../App_Themes/Tema2/Images/find.png" />						
					</td>
				</tr>
			</table>
		</div>
		<div>		
		<telerik:RadGrid ID="rgConsulta" runat="server" AllowSorting="True" Width="595px"
                    AutoGenerateColumns="False" Skin="Default" AllowPaging="True" CellSpacing="0" 
					GridLines="None" DataSourceID="ObjectDataSource1" >					
					<MasterTableView DataKeyNames="TRNOMBRE,TRCODNIT">
						<Columns>
							<telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
							<telerik:GridBoundColumn Resizable="true" SortExpression="TRCODNIT" HeaderText="No. Identificacion" 
								HeaderButtonType="TextButton" DataField="TRCODNIT" ItemStyle-HorizontalAlign="Right"
								HeaderStyle-Width="80px">
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBRE" HeaderText="Nombre" 
								HeaderButtonType="TextButton" DataField="TRNOMBRE" ItemStyle-HorizontalAlign="Right"
								HeaderStyle-Width="80px">
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
		</telerik:RadGrid>
		
		<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
						SelectMethod="GetTerceros" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
						<SelectParameters>
							<asp:Parameter Name="connection" Type="String" />
							<asp:Parameter Name="filter" Type="String" DefaultValue="1=0"/>
							<%--<asp:SessionParameter Name="filter" Type="String" SessionField="filter" DefaultValue="1=0" />--%>
							<asp:Parameter Name="startRowIndex" Type="Int32" />
							<asp:Parameter Name="maximumRows" Type="Int32" />
							<%--<asp:Parameter Name="servicio" Type="Int32" />--%>
						</SelectParameters>
					</asp:ObjectDataSource>
		</div>
		
	</fieldset>
	</telerik:RadAjaxPanel>
	<div>
			<asp:Button ID="btnBuscar" runat="server" Text="Aceptar" onclick="btnBuscar_click"/>
			<asp:Button ID="btnCancel" runat="server" Text="Cancelar" onclick="btnCancel_click"/>
		</div>
</asp:Panel>

<asp:ImageButton ID="iBtnFind" runat="server" SkinID="SkinFindUC" />
<cc1:ModalPopupExtender ID="ModalPopupFinder" runat="server" BackgroundCssClass="modalBackground"
	PopupControlID="pnFinder" TargetControlID="iBtnFind" CancelControlID="btnCancel">
</cc1:ModalPopupExtender>
