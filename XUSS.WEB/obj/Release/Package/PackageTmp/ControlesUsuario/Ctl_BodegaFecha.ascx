<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ctl_BodegaFecha.ascx.cs"
	Inherits="XUSS.WEB.ControlesUsuario.Ctl_BodegaFecha" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel ID="pnFinder" runat="server" CssClass="modalPopup" Style="display: none;
	width: 600px !important; z-index: 1003 !important;">
	<telerik:RadAjaxPanel ID="rapVisitas" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
		<fieldset class="cssFieldSetContainer" style="width: 590px !important">
			<div class="box">
				<div class="title">
					<h5>
						Parametros de Busqueda</h5>
				</div>
			</div>
			<div style="width: 100%">
				<table>
					<tr>
						<td>
							<label>
								Almacen</label>
						</td>
						<td colspan="3">
							<telerik:RadComboBox ID="rcb_bodega" runat="server" DataTextField="BDNOMBRE" DataValueField="BDBODEGA" DataSourceID="ObjectDataSource1"
							AppendDataBoundItems="true" Width="100%">
							<Items>
								<telerik:RadComboBoxItem Text="Seleccione" Value="" />
							</Items>
							</telerik:RadComboBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Fecha Inicial</label>
						</td>
						<td>
							<telerik:RadDatePicker ID="rdpFecIni" runat="server" Culture="es-CO" Width="90%"/>
						</td>
						<td>
							<label>
								Fecha Final</label>
						</td>
						<td>
							<telerik:RadDatePicker ID="rdpFecFin" runat="server" Culture="es-CO" Width="90%"/>
						</td>
					</tr>
				</table>
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

<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
	SelectMethod="GetBodegas" TypeName="XUSS.BLL.Consultas.ConsultasBL">
	<SelectParameters>
		<asp:Parameter Name="connection" Type="String" />
		<asp:Parameter Name="filter" Type="String" />
		<asp:Parameter Name="startRowIndex" Type="Int32" />
		<asp:Parameter Name="maximumRows" Type="Int32" />
	</SelectParameters>
</asp:ObjectDataSource>
