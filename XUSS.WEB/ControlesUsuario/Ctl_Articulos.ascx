<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ctl_Articulos.ascx.cs"
	Inherits="XUSS.WEB.ControlesUsuario.Ctl_Articulos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel ID="pnFinder" runat="server" CssClass="modalPopup" Style="display: none;
	width: 600px !important; z-index: 1003 !important;">
	<telerik:RadAjaxPanel ID="rapVisitas" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
		<fieldset class="cssFieldSetContainer" style="width: 590px !important">
			<div class="box">
				<div class="title">
					<h5>
						Buscar Ariculos</h5>
				</div>
			</div>
			<div style="width: 100%">
				<table>
					<tr>
						<td>
							<label>
								Linea</label>
						</td>
						<td colspan="3">
							<telerik:RadComboBox ID="rcb_TipPro" runat="server" Culture="es-CO" Width="400px"
								DataTextField="TANOMBRE" DataValueField="TATIPPRO" DataSourceID="ObjectDataSource2"
								AppendDataBoundItems="true">
								<Items>
									<telerik:RadComboBoxItem Text="Seleccione" Value="" />
								</Items>
							</telerik:RadComboBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Clave 1</label>
						</td>
						<td>
							<telerik:RadTextBox ID="txt_clave1" runat="server">
							</telerik:RadTextBox>
						</td>
						<td>
							<label>
								Clave 2</label>
						</td>
						<td>
							<telerik:RadTextBox ID="txt_clave2" runat="server">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Clave 3</label>
						</td>
						<td>
							<telerik:RadTextBox ID="txt_clave3" runat="server">
							</telerik:RadTextBox>
						</td>
						<td>
							<label>
								Clave 4</label>
						</td>
						<td>
							<telerik:RadTextBox ID="txt_clave4" runat="server">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<asp:ImageButton ID="ImageButton1" runat="server" OnClick="btn_find_Click" ImageUrl="../App_Themes/Tema2/Images/find.png" />
						</td>
					</tr>
				</table>
				<div>
					<telerik:RadGrid ID="rgConsulta" runat="server" AllowSorting="True" Width="595px"
						AutoGenerateColumns="False" Skin="Default" AllowPaging="True" CellSpacing="0" GridLines="None"
						DataSourceID="ObjectDataSource1">
						<MasterTableView DataKeyNames="ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4">
							<Columns>
								<telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
									<HeaderStyle Width="40px" />
								</telerik:GridButtonColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARTIPPRO" HeaderText="Linea"
									HeaderButtonType="TextButton" DataField="ARTIPPRO" ItemStyle-HorizontalAlign="Right"
									HeaderStyle-Width="80px">
								</telerik:GridBoundColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE1" HeaderText="Clave 1"
									HeaderButtonType="TextButton" DataField="ARCLAVE1" ItemStyle-HorizontalAlign="Right"
									HeaderStyle-Width="80px">
								</telerik:GridBoundColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE2" HeaderText="Clave 2"
									HeaderButtonType="TextButton" DataField="ARCLAVE2" ItemStyle-HorizontalAlign="Right"
									HeaderStyle-Width="80px">
								</telerik:GridBoundColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE3" HeaderText="Clave 3"
									HeaderButtonType="TextButton" DataField="ARCLAVE3" ItemStyle-HorizontalAlign="Right"
									HeaderStyle-Width="80px">
								</telerik:GridBoundColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE4" HeaderText="Clave 4"
									HeaderButtonType="TextButton" DataField="ARCLAVE4" ItemStyle-HorizontalAlign="Right"
									HeaderStyle-Width="80px">
								</telerik:GridBoundColumn>
								<telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
									HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
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
						SelectMethod="GetArticulos" TypeName="XUSS.BLL.Consultas.ConsultasBL">
						<SelectParameters>
							<asp:Parameter Name="connection" Type="String" />
							<asp:Parameter Name="filter" Type="String" DefaultValue="1=0" />
							<asp:Parameter Name="startRowIndex" Type="Int32" />
							<asp:Parameter Name="maximumRows" Type="Int32" />
						</SelectParameters>
					</asp:ObjectDataSource>
					<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
						SelectMethod="GetTipPro" TypeName="XUSS.BLL.Consultas.ConsultasBL">
						<SelectParameters>
							<asp:Parameter Name="connection" Type="String" />
							<asp:Parameter Name="filter" Type="String" />
							<asp:Parameter Name="startRowIndex" Type="Int32" />
							<asp:Parameter Name="maximumRows" Type="Int32" />
						</SelectParameters>
					</asp:ObjectDataSource>
				</div>
			</div>
		</fieldset>
	</telerik:RadAjaxPanel>
	<div>
		<asp:Button ID="btnBuscar" runat="server" Text="Aceptar" OnClick="btnBuscar_click" />
		<asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_click" />
	</div>
</asp:Panel>
<asp:ImageButton ID="iBtnFind" runat="server" SkinID="SkinFindUC" />
<cc1:ModalPopupExtender ID="ModalPopupFinder" runat="server" BackgroundCssClass="modalBackground"
	PopupControlID="pnFinder" TargetControlID="iBtnFind" CancelControlID="btnCancel">
</cc1:ModalPopupExtender>
